import Home from "./Components/Home";
import NavBar from "./Components/NavBar";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import ViewGraph from "./Components/ViewGraph";
import UploadFile from "./Components/UploadFile";

function App() {
  return (
        <Router>
            <NavBar />
            <Routes>
                <Route path='/' element={<Home />} caseSensitive />
                <Route path='/view-graph' element={<ViewGraph />} />
                <Route path='/upload-file' element={<UploadFile />} />
            </Routes>
        </Router>
    );
}

export default App;
