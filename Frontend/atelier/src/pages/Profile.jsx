import { useContext, useEffect, useState } from "react";
import secureLocalStorage from "react-secure-storage";
import { useTranslation } from "react-i18next";
import { AuthContext } from "../app/providers/context/AuthProvider.jsx";
import { useFetching } from "../shared/hooks/useFetching.jsx";
import Loader from "../shared/ui/loader/Loader.jsx";
import ProfilePageService from "api/Atelier/ProfilePageService.js";

export default function Profile() {
  const { t } = useTranslation();
  const [user, setUser] = useState(null);
  const { setIsAuth, setloggedOut } = useContext(AuthContext);

  const token = secureLocalStorage.getItem("token");

  const [fetchUserInfo, isLoading, error] = useFetching(async () => {
    const response = await ProfilePageService.GetUserInfo(token);
    console.log(response);
    setUser(response.data);
  });

  useEffect(() => {
    if (token) {
      fetchUserInfo();
    }
    // eslint-disable-next-line
  }, [token]);

  const logout = () => {
    secureLocalStorage.clear();
    setloggedOut(true);
    setIsAuth(false);
  };

  return (
    <div>
      <div>
        <h3>{t("Profile")}</h3>
      </div>
      <div>
        {isLoading && <Loader />}
        {error && <p style={{ color: "red" }}>{error}</p>}
        {user && (
          <div>
            <p>
              <strong>{t("Email")}:</strong> {user.email}
            </p>
          </div>
        )}
      </div>
      <div>
        <button onClick={logout}>{t("LogOut")}</button>
      </div>
    </div>
  );
}
