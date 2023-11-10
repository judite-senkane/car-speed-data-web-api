import { useState } from "react";
import axios, { AxiosResponse } from "axios";
import { GraphData } from "../Types/GraphData";
import Chart from "react-apexcharts";

function ViewGraph() {
  const [filterDate, setFilterDate] = useState(null);
  const [hourList, setHourList] = useState<number[]>([]);
  const [speedList, setSpeedList] = useState<number[]>([]);

  const fetchData = async () => {
    try {
      const response: AxiosResponse<GraphData[]> = await axios.get<GraphData[]>(
        "http://localhost:5000/User/day-speed-average",
        {
          params: {
            day: filterDate,
          },
        }
      );

      const hours = response.data.map((data) => data.hour);
      const speeds = response.data.map((data) => data.averageSpeed);

      setHourList(hours);
      setSpeedList(speeds);
      
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
        categories: hourList,
      },
    },
    series: [
      {
        name: "Speed km/h",
        data: speedList,
      },
    ],
  };

  return (
    <div className="ViewGraph text-center">
      <h2 className="p-4">View Speed Graph</h2>
      <input
        type="date"
        placeholder="Date from"
        value={filterDate ? filterDate : ""}
        onChange={handleFilterDateChange}
      />
      <button className="m-4 btn btn-success" onClick={() => handleSubmit()}>
        Submit
      </button>
      <div className="row">
        <div className="d-flex justify-content-center mixed-chart">
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
