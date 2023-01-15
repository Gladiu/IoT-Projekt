package com.example.lab7

import android.graphics.Color
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.LinearLayout
import androidx.fragment.app.Fragment
import com.android.volley.Request
import com.android.volley.toolbox.JsonArrayRequest
import com.android.volley.toolbox.Volley
import com.google.android.material.textfield.TextInputEditText
import com.jjoe64.graphview.GraphView
import com.jjoe64.graphview.series.DataPoint
import com.jjoe64.graphview.series.LineGraphSeries

/**
 * @brief Viewmodel of Graph classes
 *
 * @description This class is a viewmodel used in View creating graphs.
 *
 * @property GraphModelObject object used to encapsulate backend data
 * @property graphTemperature graph objects used to display temperature data
 * @property graphPressure graph objects used to display pressure data
 * @property graphHumidity graph objects used to display humidity data
 * @constructor NA
 */

class GraphViewModel : Fragment() {

    val GraphModelObject: GraphModel = GraphModel()

    lateinit var graphView : GraphView
    lateinit var changeUnitsButton: Button

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    /**
     * Creating and filling the view with graphs
     */
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_graph, container, false)

        GraphModelObject.initSettings(this.requireActivity())

        graphView = view.findViewById(R.id.graphView)

        changeUnitsButton = view.findViewById(R.id.changeUnitsButton)

        graphView.legendRenderer.isVisible = true
        graphView.legendRenderer.setFixedPosition(4, 5);
        graphView.legendRenderer.setTextSize(20F);

        graphView.setOnClickListener {
                GraphModelObject.cycleCurrentData(graphView)
        }

        changeUnitsButton.setOnClickListener {
            GraphModelObject.changeUnits(graphView)
        }

        GraphModelObject.startTimer(this, graphView)

        return view

    }

}