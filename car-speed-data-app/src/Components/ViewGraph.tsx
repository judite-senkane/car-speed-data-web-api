import { useState } from "react";
import axios, { AxiosResponse } from "axios";
import { AverageSpeedResult } from "../Types/AverageSpeedResult";
import Chart from "react-apexcharts";

function ViewGraph() {
  const [filterDate, setFilterDate] = useState(null);
  const [averageSpeedData, setAverageSpeedData] = useState<number[]>([]);

  const fetchData = async () => {
    try {
      const response: AxiosResponse<AverageSpeedResult> =
        await axios.get<AverageSpeedResult>(
          "http://localhost:5150/User/day-speed-average",
          {
            params: {
              day: filterDate,
            },
          }
        );

      setAverageSpeedData(response.data.speed);
    } catch (error: any) {
      console.error("Error fetching car speed data:", error.message);
    }
  };

  const handleFilterDateChange = (event: any) => {
    setFilterDate(event.target.value);
  };

  const handleSubmit = () => {
    fetchData();
  };

  const state = {
    options: {
      chart: {
        id: "line",
      },
      xaxis: {
        categories: [
          1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
          21, 22, 23, 24,
        ],
      },
    },
    series: [
      {
        name: "Speed km/h",
        data: averageSpeedData,
      },
    ],
  };

  return (
    <div className="ViewGraph">
      <h2>View Speed Graph</h2>
      <input
        type="date"
        placeholder="Date from"
        value={filterDate ? filterDate : ""}
        onChange={handleFilterDateChange}
      />
      <button className="btn" onClick={() => handleSubmit()}>
        Submit
      </button>
      <div className="row">
        <div className="mixed-chart">
          <Chart
            options={state.options}
            series={state.series}
            type="line"
            width="800"
          />
        </div>
      </div>
    </div>
  );
}

export default ViewGraph;
