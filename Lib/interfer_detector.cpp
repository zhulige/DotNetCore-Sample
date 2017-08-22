#include "stdafx.h"
#include "interfer_detector.h"
#include <math.h>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
//#include <sys/time.h>
//#include <unistd.h>

int InterferDetector::s_energy_l1_buffer_size = 0;//1级能量缓冲大小
int InterferDetector::s_energy_l2_buffer_size = 0;//2级能量缓冲大小
int InterferDetector::s_interfer_detect_window_size = 0;//滑动窗口大小
int InterferDetector::s_frequency_monitor_period = 0;//信号强度监测周期
int InterferDetector::s_threshold_for_frequency = 0;//拐点频率门限值
int InterferDetector::s_threshold_for_integral = 0;//信号强度积分门限值
int InterferDetector::s_threshold_of_slope = 0;//两次拐点间隔门限值
int InterferDetector::s_frequency_weight = 0;//频率权重
int InterferDetector::s_centre_frequency = 0;//对频率进行归一化处理
double InterferDetector::s_scale = 0;//权重系数

const char* interfer_detector_version = "3E";

InterferDetector::InterferDetector(void)
{
	Init();
}

InterferDetector::~InterferDetector(void)
{
	//
}

int InterferDetector::GetCurFreq()
{
	int cur_freq = (int)frequency_counter.size();

	return cur_freq;
}

double InterferDetector::GetCurEnergy()
{
	double retVal = 0;

	if (energy_l2_buffer.size() > 0)
		retVal = energy_l2_buffer[energy_l2_buffer.size() - 1];

	return retVal;
}

long InterferDetector::GetInterfer(void)
{
	//printf("%p, %lf = ", this, interfer);
	return (long)interfer;
}

//判断是否为拐点
bool InterferDetector::IsInflexion(char current_value, char last_inflexion_value, Direction &recent_direction, int threshold)
{
    return false;
}

void InterferDetector::Init(void)
{
	sample_counter = 0;
	prev_inflexion_position_for_integral = 0;
	inflexion_pos = 0;
	dir_for_frequency = DIR_STABLE;
	dir_for_integral = DIR_STABLE;

	frequency_counter.clear();
	energy_l1_buffer.clear();
	energy_l2_buffer.clear();
	orig_rssi_buffer.clear();
	inflexion_rssi_for_energy.clear();
	prev_rssi_for_frequency = -1;
	prev_rssi_for_integral = -1;
	last_valid_rssi = 0;
	interfer = 0;
	arg_rssi_for_frequency = 0;
	inflexion_pos_rssi = 0;

	// add at 2015-07-21
	l2_last_sum_a = 0;
	l2_last_sum_b = 0;

	// 2016-04-24
	start_counter = 0;
	delta_area = 0;
	dir_for_preproccess = DIR_STABLE;
}

void InterferDetector::Reset(char rssi)
{
	prev_rssi_for_frequency = rssi;
	prev_rssi_for_integral = rssi;
	last_valid_rssi = rssi;
	prev_inflexion_position_for_integral = sample_counter;
	dir_for_frequency = DIR_STABLE;
	dir_for_integral = DIR_STABLE;

	arg_rssi_for_frequency = rssi;
	inflexion_pos_rssi = rssi;
	inflexion_rssi_for_energy.push_back(inflexion_pos_rssi);
	inflexion_pos = 1;

	fast_avg_rssi = rssi;
	slow_avg_rssi = rssi;
	start_counter = 1;
	delta_area = 0;
	prev_rssi_for_preproccess = rssi;
	dir_for_preproccess = DIR_STABLE;
	//gettimeofday(&last_time_, NULL);
}

bool InterferDetector::PushSample(char org_rssi)
{
    interfer = org_rssi / 2.0;
	//printf("%p, %lf - ", this, interfer);
	return false;
}
char InterferDetector::Prepare(char org_rssi)
{
    return org_rssi;
}

float InterferDetector::CalcScaling(char org_rssi)
{
    return SCALE_DOWN;
}

char InterferDetector::Preproccess(char pre_rssi, float scaling_ratio)
{
    return pre_rssi;
}

void InterferDetector::SetInflexionParm(char prev_rssi_for_preproccess, unsigned long long inflexion_pos_for_preproccess, Direction dir_for_preproccess)
{
}


void InterferDetector::ResetPerproccess()
{
}

void InterferDetector::SetParams(int energy_l1_buffer_size, int energy_l2_buffer_size, int interfer_detect_window_size, int frequency_monitor_period,
		int threshold_for_frequency, int threshold_for_integral, int threshold_of_slope, int frequency_weight, int centre_frequency, double scale)
{
	s_energy_l1_buffer_size = energy_l1_buffer_size;
	s_energy_l2_buffer_size = energy_l2_buffer_size;
	s_interfer_detect_window_size = interfer_detect_window_size;
	s_frequency_monitor_period = frequency_monitor_period;
	s_threshold_for_frequency = threshold_for_frequency;
	s_threshold_for_integral = threshold_for_integral;
	s_threshold_of_slope = threshold_of_slope;
	s_frequency_weight = frequency_weight;
	s_centre_frequency = centre_frequency;
	s_scale = scale;
}
const char* InterferDetector::GetVersion()
{
	return interfer_detector_version;
}
deque<char> InterferDetector::GetOrigRssi()
{
	return orig_rssi_buffer;
}
