package com.example.lab7

import com.jjoe64.graphview.series.DataPoint
import com.jjoe64.graphview.series.LineGraphSeries

/**
 * @brief Data class for Graphs
 *
 * @description Class that encapsulates all the information Graphs series
 *
 * @property measurement value holding Data for plotting graphs
 * @property name name of measurement
 * @property unit unit of measurement
 */
class GraphData(var measurement : LineGraphSeries<DataPoint> , var name : String, var unit : String)
