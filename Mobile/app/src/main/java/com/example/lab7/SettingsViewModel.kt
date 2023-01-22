package com.example.lab7

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import com.google.android.material.textfield.TextInputEditText

/**
 * @brief Viewmodel of Settings
 *
 * @description This class is used to display current application settings
 *
 * @property applyButton Button that applies current changes
 * @property IPTextInput TextInput used to change current url
 * @property SampleTimetextInput TextInput used to change Sample time of data
 * @property SettingsModelObject object used to encapsulate backend data
 * @constructor NA
 */
class SettingsViewModel : Fragment() {


    lateinit var applyButton : Button
    lateinit var IPTextInput : TextInputEditText
    lateinit var SampleTimetextInput : TextInputEditText
    val SettingsModelObject = SettingsModel()


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