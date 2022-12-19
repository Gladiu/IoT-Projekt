package com.example.lab7

import android.graphics.Color
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import androidx.fragment.app.Fragment
import com.android.volley.Request
import com.android.volley.toolbox.JsonArrayRequest
import com.android.volley.toolbox.Volley
import com.google.android.material.textfield.TextInputEditText
import com.jjoe64.graphview.GraphView
import com.jjoe64.graphview.series.DataPoint
import com.jjoe64.graphview.series.LineGraphSeries


class GraphViewModel : Fragment() {

    val GraphModelObject: GraphModel = GraphModel()

    lateinit var graphTemperature: GraphView
    lateinit var graphPressure: GraphView
    lateinit var graphHumidity: GraphView

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
        graphTemperature = view.findViewById(R.id.graphTemperature)
        graphPressure = view.findViewById(R.id.graphPressure)
        graphHumidity = view.findViewById(R.id.graphHumidity)

        GraphModelObject.startTimer(this)

        graphTemperature.addSeries(GraphModelObject.temperatureSeries)
        graphPressure.addSeries(GraphModelObject.pressureSeries)
        graphHumidity.addSeries(GraphModelObject.humiditySeries)

        graphTemperature.legendRenderer.setVisible(true);
        graphPressure.legendRenderer.setVisible(true);
        graphHumidity.legendRenderer.setVisible(true);
        // setting fix position for the title
        graphTemperature.legendRenderer.setFixedPosition(4, 5);
        graphPressure.legendRenderer.setFixedPosition(4, 5);
        graphHumidity.legendRenderer.setFixedPosition(4, 5);

        graphTemperature.legendRenderer.setTextSize(20F);
        graphPressure.legendRenderer.setTextSize(20F);
        graphHumidity.legendRenderer.setTextSize(20F);
        // on below line we are adding data to our graph view.

        cyclicTextInput = view.findViewById(R.id.cyclicTextInput)

        cycleApplyButton = view.findViewById(R.id.cycleApplyButton)
        cycleApplyButton.setOnClickListener {

            try {
                GraphModelObject.cycleTime = cyclicTextInput.text.toString().toLong()
            }catch (exc: Throwable){}

        }


        return view

    }

}