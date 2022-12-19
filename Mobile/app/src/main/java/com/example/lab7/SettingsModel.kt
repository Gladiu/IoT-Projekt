package com.example.lab7

import android.content.Context.MODE_PRIVATE
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.FragmentActivity

class SettingsModel {
    var IP:String = "http://217.182.75.146/index.php"
    var sampleTime: Long = 100L


    fun setNewSampleTime(newSampleTime:Long, currentActivity: FragmentActivity){
        // Push new IP to settings
        currentActivity.getPreferences(MODE_PRIVATE)
            .edit()
            .putLong("sampleTime", newSampleTime)
            .commit()
        sampleTime = newSampleTime
    }

    fun setNewIP(newIP:String, currentActivity: FragmentActivity){
        // Push new IP to settings
        currentActivity.getPreferences(MODE_PRIVATE)
            .edit()
            .putString("IP", newIP)
            .commit()
        IP = newIP
    }
}