import { useState, useEffect } from "react";
import axios, { AxiosResponse } from "axios";
import { CarSpeedData } from "../Types/CarSpeedData";
import { DataResult } from "../Types/DataResult";

function Home() {
  const [carSpeedDataList, setCarSpeedDataList] = useState<CarSpeedData[]>([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [filterDateFrom, setFilterDateFrom] = useState(null);
  const [filterDateTo, setFilterDateTo] = useState(null);
  const [filterSpeed, setFilterSpeed] = useState(0);

  useEffect(() => {
    fetchData();
  }, [currentPage]);

  const fetchData = async () => {
    try {
      const response: AxiosResponse<DataResult> = await axios.get<DataResult>(
        "http://localhost:5150/User/data",
        {
          params: {
            page: currentPage,
            dateFrom: filterDateFrom,
            dateTo: filterDateTo,
            speed: filterSpeed,
          },
        }
      );

      setCarSpeedDataList(response.data.items);
      setTotalPages(response.data.totalPages);
    } catch (error: any) {
      console.error("Error fetching car speed data:", error.message);
    }
  };

  const handleFilterDateFromChange = (event: any) => {
    setFilterDateFrom(event.target.value);
  };

  const handleFilterDateToChange = (event: any) => {
    setFilterDateTo(event.target.value);
  };

  const handleFilterSpeedChange = (event: any) => {
    setFilterSpeed(event.target.value);
  };

  const handlePageChange = (newPage: any) => {
    setCurrentPage(newPage);
  };

  const handleSubmit = () => {
    fetchData();
  };
  return (
    <div className="Home">
      <h1>Car Speed Data</h1>
      <label htmlFor="date-from" className="label">
        Date From
      </label>
      <input
        id="date-from"
        type="date"
        placeholder="Date from"
        value={filterDateFrom ? filterDateFrom : ""}
        onChange={handleFilterDateFromChange}
      />
      <label htmlFor="date-to" className="label">
        Date To
      </label>
      <input
        id="date-to"
        type="date"
        placeholder="Date to"
        value={filterDateTo ? filterDateTo : ""}
        onChange={handleFilterDateToChange}
      />

      <label htmlFor="speed" className="label">
        Speed
      </label>
      <input
        id="speed"
        type="number"
        placeholder="Speed"
        value={filterSpeed}
        onChange={handleFilterSpeedChange}
      />
      <button className="btn" onClick={() => handleSubmit()}>
        Apply filters
      </button>
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Date and time</th>
            <th>Speed km/h</th>
            <th>License number</th>
          </tr>
        </thead>
        <tbody>
          {carSpeedDataList.map((CarSpeedData: CarSpeedData) => (
            <tr key={CarSpeedData.id}>
              <td>{CarSpeedData.dateAndTime}</td>
              <td>{CarSpeedData.speed}</td>
              <td>{CarSpeedData.licenseNumber}</td>
            </tr>
          ))}
        </tbody>
      </table>
      <div>
        <button
          className="btn"
          onClick={() => handlePageChange(currentPage - 1)}
          disabled={currentPage === 1}
        >
          Previous Page
        </button>
        <span>
          {" "}
          Page {currentPage} of {totalPages}{" "}
        </span>
        <button
          className="btn"
          onClick={() => handlePageChange(currentPage + 1)}
          disabled={currentPage === totalPages}
        >
          Next Page
        </button>
      </div>
    </div>
  );
}

export default Home;
