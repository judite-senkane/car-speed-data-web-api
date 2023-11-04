import axios from "axios";
import { useState } from "react";

function ClearDatabase() {
  const [result, setResult] = useState("");
  const clearData = async () => {
    setResult("Clearing...");
    try {
      await axios
        .post("http://localhost:5150/Cleanup/clear")
        .then(() => setResult("Database has been cleared"))
        .then(() => window.location.reload());
    } catch (error: any) {
      console.error("Error clearing database", error.message);
    }
  };

  const handleSubmit = () => clearData();

  return (
    <div className="ClearDatabase">
      <button
        className="p-2 m-2 btn btn-danger btn-sm text-right"
        type="submit"
        onClick={() => handleSubmit()}
      >
        ClearDatabase
      </button>
      <p className="p-2">{result}</p>
    </div>
  );
}

export default ClearDatabase;
