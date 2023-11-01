import  { useState } from "react";
import axios from "axios";
import { LineChart, Line } from "recharts";

function ViewGraph() {
  const [filterDate, setFilterDate] = useState(null);
  const [averageSpeedData, setAverageSpeedData] = useState([]);

  const fetchData = async () => { 
    await axios.get("http://localhost:5150/User/day-speed-average", {params: {date: filterDate}})
        .then((response) => {
          setAverageSpeedData(response.data);
        })
        .catch((error) => {
          console.error("Error:", error);
        });
      }

  const handleFilterDateChange = (event: any) => { 
    setFilterDate(event.target.value)
  };

  const handleSubmit = () => {
    fetchData();
  }

  return (
    <div className="ViewGraph">
      <h2>View Speed Graph</h2>
      <input
        type="date"
        placeholder="Date from"
        value={filterDate ? filterDate : "" }
        onChange={handleFilterDateChange}
      />
      <button className="btn" type="submit" onSubmit={handleSubmit}>Submit</button>
      <div>
        <LineChart width={400} height={400} data={averageSpeedData}>
          <Line type="monotone" dataKey="uv" stroke="#8884d8" />
        </LineChart>
      </div>
    </div>
  );
}

export default ViewGraph;
