import { useState } from "react";
import axios from "axios";

function UploadFile() {
  const [addedFile, setAddedFile] = useState<File | string>("");
  const [result, setResult] = useState("");

  const handleUpload = async () => {
    setResult("Loading...");
    const formData = new FormData();
    formData.append("file", addedFile);
    axios
      .post("http://localhost:5150/FileUpload/upload", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      })
      .then(() => setResult("File uploaded."));
  };

  const handleAddedFile = (event: any) => {
    setAddedFile(event.target.files[0]);
  };

  const handleSubmit = () => {
    handleUpload();
  };

  return (
    <div className="UploadFile">
      <h3 className="p-3">File Upload</h3>
      <p className="ps-3">{result}</p>
      <label className="p-3 btn btn-default p-0">
        <input type="file" onChange={handleAddedFile} />
      </label>
      <button
        className="pe-3 ps-3 btn btn-success btn-sm"
        type="submit"
        onClick={() => handleSubmit()}
      >
        Upload
      </button>
    </div>
  );
}

export default UploadFile;
