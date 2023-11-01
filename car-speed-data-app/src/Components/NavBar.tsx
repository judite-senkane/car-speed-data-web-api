function NavBar() {
return (
  <div className="NavBar">
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
      <a className="navbar-brand" href="/">
        Menu
      </a>
      <button
        className="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#navbarNav"
        aria-controls="navbarNav"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span className="navbar-toggler-icon"></span>
      </button>
      <div className="collapse navbar-collapse" id="navbarNav">
        <ul className="navbar-nav">
          <li className="nav-item active">
            <a className="nav-link" href="/">
              Home
            </a>
          </li>
          <li className="nav-item">
            <a className="nav-link" href="/view-graph">
              View Graph
            </a>
          </li>
          <li className="nav-tem">
            <a className="nav-link" href="/upload-file">
              Upload File
            </a>
          </li>
        </ul>
      </div>
    </nav>
  </div>
);
}
export default NavBar;