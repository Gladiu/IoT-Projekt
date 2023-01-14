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
    var IP:String = "https://b6bd4311-6494-495a-a73c-25ae508bb185.mock.pstmn.io"
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