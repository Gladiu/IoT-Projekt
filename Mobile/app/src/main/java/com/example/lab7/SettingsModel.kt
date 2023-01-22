package com.example.lab7

import android.content.Context.MODE_PRIVATE
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.FragmentActivity

/**
 * @brief Model of settings classes
 *
 * @description This class is a model used to manage settings
 *
 * @property IP default IP
 * @property sampleTime default sample time
 */
class SettingsModel {
    var IP:String = "http://192.168.1.5:5000"
    var sampleTime: Long = 100L


    /**
     * Sets new sample time in preferences
     */
    fun setNewSampleTime(newSampleTime:Long, currentActivity: FragmentActivity){
        // Push new IP to settings
        currentActivity.getPreferences(MODE_PRIVATE)
            .edit()
            .putLong("sampleTime", newSampleTime)
            .commit()
        sampleTime = newSampleTime
    }

    /**
     * Sets new ip in preferences
     */
    fun setNewIP(newIP:String, currentActivity: FragmentActivity){
        // Push new IP to settings
        currentActivity.getPreferences(MODE_PRIVATE)
            .edit()
            .putString("IP", newIP)
            .commit()
        IP = newIP
    }
}