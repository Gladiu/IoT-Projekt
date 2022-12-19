package com.example.lab7

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import com.google.android.material.textfield.TextInputEditText

class SettingsViewModel : Fragment() {

    lateinit var applyButton : Button
    lateinit var IPTextInput : TextInputEditText
    lateinit var SampleTimetextInput : TextInputEditText
    val SettingsModelObject = SettingsModel()


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_settings, container, false)

        IPTextInput = view.findViewById(R.id.textInputIP)
        IPTextInput.setText(SettingsModelObject.IP)

        SampleTimetextInput = view.findViewById(R.id.textInputSampleTime)
        SampleTimetextInput.setText(SettingsModelObject.sampleTime.toString())

        applyButton = view.findViewById(R.id.applyButton)
        applyButton.setOnClickListener {
            try {
                SettingsModelObject.setNewIP(IPTextInput.text.toString(), requireActivity())
                SettingsModelObject.setNewSampleTime(IPTextInput.text.toString().toLong(), requireActivity())
            }catch (exc: Throwable){}
        }

        return view
    }

}