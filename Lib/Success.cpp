// Win32Project1.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Success.h"


// This is an example of an exported function.
Export_API int GSuccess()
{
	return 1000;
}


#include "interfer_detector.h"
#include <stdio.h>


Export_API InterferDetector * NewInterferDetector()
{
	return new InterferDetector();
}

Export_API void SetParams(InterferDetector * interfer)
{
	((InterferDetector *)interfer)->SetParams(100, 200, 100, 200, 2, 4, 20, 2, 5, 20);
}

//Push rssi
Export_API void PushSample(InterferDetector *interfer, char rssi)
{
	((InterferDetector *)interfer)->PushSample(-rssi % 10 - 50);
}

//获取干扰值
Export_API double GetInterfer(InterferDetector *interfer)
{
	return ((InterferDetector *)interfer)->GetInterfer();
}

