package com.example.lab7

import android.graphics.Color
import android.os.Bundle
import android.provider.ContactsContract.Data
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import com.android.volley.Request
import com.android.volley.toolbox.JsonArrayRequest
import com.android.volley.toolbox.JsonObjectRequest
import com.android.volley.toolbox.Volley
import com.google.android.material.textfield.TextInputEditText
import com.jjoe64.graphview.GraphView
import com.jjoe64.graphview.series.DataPoint
import com.jjoe64.graphview.series.LineGraphSeries
import kotlinx.coroutines.*


class Graph : Fragment() {

    lateinit var graph: GraphView
    var currentTime = 0.0
    var lastTime = 0.0
    var cycleTime = 100L // TODO: Chagne name

    lateinit var temperatureSeries: LineGraphSeries<DataPoint>
    lateinit var pressureSeries: LineGraphSeries<DataPoint>
    lateinit var humiditySeries: LineGraphSeries<DataPoint>

    lateinit var cycleApplyButton: Button
    lateinit var cyclicTextInput: TextInputEditText

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_graph, container, false)
        graph = view.findViewById(R.id.graph)


        temperatureSeries= LineGraphSeries( arrayOf(DataPoint(0.0,0.0)))
        pressureSeries= LineGraphSeries( arrayOf(DataPoint(0.0,0.0)))
        humiditySeries= LineGraphSeries( arrayOf(DataPoint(0.0,0.0)))
        temperatureSeries.title = "Temperature [C]"
        pressureSeries.title = "Pressure [hPa]"
        humiditySeries.title = "Humidity [%]"
        graph.addSeries(temperatureSeries)
        graph.addSeries(pressureSeries)
        graph.addSeries(humiditySeries)
        temperatureSeries.color = Color.BLUE
        pressureSeries.color = Color.GREEN
        humiditySeries.color = Color.RED


        graph.legendRenderer.setVisible(true);
        // setting fix position for the title
        graph.legendRenderer.setFixedPosition(4, 5);
        graph.legendRenderer.setTextColor(Color.BLUE);
        graph.legendRenderer.setTextSize(20F);
        // on below line we are adding data to our graph view.

        cyclicTextInput = view.findViewById(R.id.cyclicTextInput)

        cycleApplyButton = view.findViewById(R.id.cycleApplyButton)
        cycleApplyButton.setOnClickListener {

            cycleTime = cyclicTextInput.text.toString().toLong()

        }

        val timerName = lifecycleScope.launch(Dispatchers.IO) {
            while (isActive) {
                lifecycleScope.launch {
                    //doSomething()
                        val volleyQueue = Volley.newRequestQueue(activity)
                        val url = "http://217.182.75.146/index.php" // TODO: Change for release

                        val jsonObjectRequest = JsonArrayRequest(
                            Request.Method.GET,
                            url,
                            null,
                            {
                                    response ->
                                if (lastTime < currentTime) {
                                        temperatureSeries.appendData(
                                            DataPoint(
                                                currentTime/1000,
                                                response.getJSONObject(1).getDouble("value")
                                            ),
                                            false, 10
                                    )
                                        pressureSeries.appendData(
                                            DataPoint(
                                                currentTime/1000,
                                                response.getJSONObject(2).getDouble("value")
                                            ),
                                            false, 10
                                        )
                                        humiditySeries.appendData(
                                            DataPoint(
                                                currentTime/1000,
                                                response.getJSONObject(3).getDouble("value")
                                            ),
                                            false, 10
                                        )
                                    lastTime = currentTime
                                }

                            },
                            {
                                error ->
                                print("Error Occured something wrong with http request")
                            }
                        )
                        volleyQueue.add(jsonObjectRequest)
                    }
                currentTime += cycleTime
                delay(cycleTime)
            }
        }

        return view

    }

}