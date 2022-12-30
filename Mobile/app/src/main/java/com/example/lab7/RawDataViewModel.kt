package com.example.lab7

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.LinearLayout
import android.widget.TextView

class RawDataViewModel : Fragment() {

    lateinit var rawDataLinearLayout: LinearLayout

    lateinit var temperatureTextView: TextView
    lateinit var pressureTextView: TextView
    lateinit var humidityTextView: TextView
    lateinit var rollTextView: TextView
    lateinit var pitchTextView: TextView
    lateinit var yawTextView: TextView

    var RawDataModel = RawDataModel()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

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