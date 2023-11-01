import { useState, useEffect } from "react";
import axios, { AxiosResponse } from "axios";


function UploadFile() {
  const [addedFile, setAddedFile] = useState(null);
  const [result, setResult] = useState("");
  const [submit, setSubmit] = useState(false);
  
   useEffect(() => { 
    axios.get("http://localhost:5150/FileUpload/upload", {params: {file: addedFile}})
        .then(() => setResult("File uploaded."))
        }, [submit === true]);

  const handleAddedFile = (event: any) => {
    setAddedFile(event.target.value)
  }

    const handleSubmit = () => {
      setSubmit(true);
    };


  return (
    <div className="UploadFile">
      <h3>File Upload</h3>
      <p>{result}</p>
      <label className="btn btn-default p-0">
        <input type="file" onChange={handleAddedFile} />
      </label>
      <button
        className="btn btn-success btn-sm"
        type="submit"
        onClick={handleSubmit}
      >
        Upload
      </button>
    </div>
  );
}

export default UploadFile;