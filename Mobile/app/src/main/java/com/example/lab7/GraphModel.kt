package com.example.lab7

import android.content.Context
import android.graphics.Color
import androidx.fragment.app.Fragment
import com.android.volley.Request
import com.android.volley.toolbox.JsonArrayRequest
import com.android.volley.toolbox.Volley
import com.jjoe64.graphview.series.DataPoint
import com.jjoe64.graphview.series.LineGraphSeries
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.isActive
import kotlinx.coroutines.launch
import androidx.lifecycle.lifecycleScope
import kotlinx.coroutines.*

class GraphModel {
    var currentTime = 0.0
    var lastTime = 0.0
    var cycleTime = 100L // TODO: Chagne name

    var temperatureSeries: LineGraphSeries<DataPoint>
    var pressureSeries: LineGraphSeries<DataPoint>
    var humiditySeries: LineGraphSeries<DataPoint>

    init {

        temperatureSeries = LineGraphSeries(arrayOf(DataPoint(0.0, 0.0)))
        pressureSeries = LineGraphSeries(arrayOf(DataPoint(0.0, 0.0)))
        humiditySeries = LineGraphSeries(arrayOf(DataPoint(0.0, 0.0)))
        temperatureSeries.title = "Temperature [C]"
        pressureSeries.title = "Pressure [hPa]"
        humiditySeries.title = "Humidity [%]"

        temperatureSeries.color = Color.BLUE
        pressureSeries.color = Color.GREEN
        humiditySeries.color = Color.RED
    }

    fun startTimer(currentFragment: Fragment) {
        val timerName = currentFragment.lifecycleScope.launch(Dispatchers.IO) {
            while (isActive) {
                currentFragment.lifecycleScope.launch {
                    //doSomething()
                    val volleyQueue = Volley.newRequestQueue(currentFragment.requireActivity())
                    var url = currentFragment.requireActivity().getPreferences(Context.MODE_PRIVATE).getString("IP","")
                    url += "/get/DataObjects"

                    cycleTime = currentFragment.requireActivity().getPreferences(Context.MODE_PRIVATE).getLong("sampleTime",0)

                    val jsonObjectRequest = JsonArrayRequest(
                        Request.Method.GET,
                        url,
                        null,
                        { response ->
                            if (lastTime < currentTime) {
                                temperatureSeries.appendData(
                                    DataPoint(
                                        currentTime / 1000,
                                        response.getJSONObject(1).getDouble("value")
                                    ),
                                    false, 10
                                )
                                pressureSeries.appendData(
                                    DataPoint(
                                        currentTime / 1000,
                                        response.getJSONObject(2).getDouble("value")
                                    ),
                                    false, 10
                                )
                                humiditySeries.appendData(
                                    DataPoint(
                                        currentTime / 1000,
                                        response.getJSONObject(3).getDouble("value")
                                    ),
                                    false, 10
                                )
                                lastTime = currentTime
                            }

                        },
                        { error ->
                            print("Error Occured something wrong with http request")
                        }
                    )
                    volleyQueue.add(jsonObjectRequest)
                }
                currentTime += cycleTime
                delay(cycleTime)
            }
        }
    }
}