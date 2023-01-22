package com.example.lab7

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.LinearLayout
import android.widget.TextView

/**
 * @brief Viewmodel of Raw Data classes
 *
 * @description This class is a viewmodel used to display raw data from sensors
 *
 * @property RawDataModel object used to encapsulate backend data
 * @property rawDataLinearLayout LinearLayout holding all of the raw data textviews
 * @constructor NA
 */

class RawDataViewModel : Fragment() {

    var RawDataModel = RawDataModel()

    lateinit var rawDataLinearLayout: LinearLayout

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
        val view = inflater.inflate(R.layout.fragment_raw_data, container, false)

        rawDataLinearLayout = view.findViewById(R.id.rawDataLinearLayout)

        RawDataModel.initSettings(requireActivity())
        RawDataModel.startTimer(this, rawDataLinearLayout)

        return view
    }

}