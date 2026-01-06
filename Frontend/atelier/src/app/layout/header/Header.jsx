import Navbar from "../header/navbar/Navbar.jsx";
import Logo from "../header/logo/Logo.jsx";
import Sidebar from "../header/sidebar/Sidebar.jsx";
import Dropdown from "../../../shared/ui/dropdown/Dropdown.jsx";
import Search from "../header/search/Search.jsx";
import Language from "../header/language/Language.jsx";
import Login from "../header/login/Login.jsx";
import Slider from "../../../shared/ui/slider/Slider.jsx";
import mcss from "./Header.module.css";

export default function Header() {
  return (
    <div className="banner_bg_main">
      <Navbar />
      <Logo />
      <div className={mcss.headerSection}>
        <div className="container">
          <div className={mcss.containtMain}>
            <Sidebar />
            <Dropdown />
            <Search />
            <div className="header_box">
              <Language />
              <Login />
            </div>
          </div>
        </div>
      </div>
      <Slider />
    </div>
  );
}