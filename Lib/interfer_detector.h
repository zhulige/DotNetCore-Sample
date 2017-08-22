/********************************************************************
 created:	2015/05/20
 modify :    2015/05/20
 file :      interfer_detector.h
 author:		yuanzy
 purpose:    detector for detect the interfer
 *********************************************************************/

#ifndef INTERFER_DETECTOR_H
#define INTERFER_DETECTOR_H

#define VALID_TIME              2000            //有效时延2s
#define FAST_AVG_PROCCESS_NUM   25              //n=25，在12ms的情况下，相当于300ms
#define SLOW_AVG_PROCCESS_NUM   2000            //n=2000，在12ms的情况下，相当于24000ms
#define STABLE_AVG_NUM          50
#define SCALE_DOWN              0.5
#define BUFFER1_SIZE            400
#define BUFFER2_SIZE            800
#define RATIO                   1                //缩减比例系数
#define THRESHOLD_01            (BUFFER1_SIZE / 200 * 3)
#define THRESHOLD_02            (BUFFER2_SIZE / 200 * 3)
#define BUFFER1_HALF_SIZE        BUFFER1_SIZE / 2
#define BUFFER2_HALF_SIZE        BUFFER2_SIZE / 2
#define WAITING_SIZE             BUFFER2_SIZE //max(SLOW_AVG_PROCCESS_NUM,BUFFER2_SIZE)

#include <deque>
using namespace std;
//#include <sys/time.h>

enum Direction
{
	DIR_UP = 0,
	DIR_DOWN = 1,
	DIR_STABLE = 2,
};

struct InflexionInfo
{
	char rssi;
	unsigned long long pos_index;
	Direction dir;
};

class InterferDetector
{
public:
	InterferDetector(void);
	~InterferDetector(void);

	int GetCurFreq(void);
	double GetCurEnergy(void);
	double GetInterfer(void);

	bool PushSample(char rssi);

	void Init(void);
	void Reset(char rssi);

	deque<char> GetOrigRssi();

	static void SetParams(int energy_l1_buffer_size,
					 	  int energy_l2_buffer_size,
					 	  int interfer_detect_window_size,
					 	  int frequency_monitor_period,
					 	  int threshold_for_frequency,
					 	  int threshold_for_integral,
					 	  int threshold_of_slope,
					 	  int frequency_weight,
					 	  int centre_frequency,
					 	  double scale);
	static const char* GetVersion();

private:
	 bool IsInflexion(char current_value,
			 	 	  char last_inflexion_value,
			 	 	  Direction &recent_direction,
			 	 	  int threshold);

	 char Prepare(char org_rssi);
	 void ResetPerproccess();
	 float CalcScaling(char pre_rssi);
	 char Preproccess(char pre_rssi, float scaling_ratio);
	 void SetInflexionParm(char prev_rssi_for_preproccess,
			 unsigned long long inflexion_pos_for_preproccess,
			 Direction dir_for_preproccess);

protected:
	Direction dir_for_frequency;
	Direction dir_for_integral;
	char prev_rssi_for_frequency;//累计次数
	char prev_rssi_for_integral;//上一个积分信号值
	char inflexion_pos_rssi;
	double arg_rssi_for_frequency;
	char last_valid_rssi;
	unsigned long long prev_inflexion_position_for_integral;//上一次计算积分时拐点位置
	deque<unsigned long long> frequency_counter;//累计次数队列
	deque<char> orig_rssi_buffer;
	deque<double> energy_l1_buffer;
	deque<double> energy_l2_buffer;
	deque<char> inflexion_rssi_for_energy;
	unsigned long long sample_counter;
	unsigned long long inflexion_pos;
	double interfer;

	// add by yuanzy at 2015-07-21
	double l2_last_sum_a;
	double l2_last_sum_b;

	// 2016-04-24
	//timeval last_time_;
	float fast_avg_rssi;
	float slow_avg_rssi;
	float delta_area;
	char prev_rssi_for_preproccess;
	Direction dir_for_preproccess;
	int buffer1_inflexion_num;
	int buffer2_inflexion_num;
	int buffer3_inflexion_num;
	int buffer4_inflexion_num;
	unsigned long long start_counter;
	deque<InflexionInfo> inflexion_counter;
	deque<float> scaling_counter;

	static int s_energy_l1_buffer_size;
	static int s_energy_l2_buffer_size;
	static int s_interfer_detect_window_size;
	static int s_frequency_monitor_period;
	static int s_threshold_for_frequency; 	//计数门限?
	static int s_threshold_for_integral;	//积分门限
	static int s_threshold_of_slope;		//斜率门限
	static int s_frequency_weight;
	static int s_centre_frequency;
	static double s_scale;
};

#endif	//INTERFER_DETECTOR_H
