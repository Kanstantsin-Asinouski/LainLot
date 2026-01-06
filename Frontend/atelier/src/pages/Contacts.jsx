import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import ContactsPageService from "api/Atelier/ContactsPageService.js";
import { useFetching } from "../shared/hooks/useFetching.jsx";
import Loader from "../shared/ui/loader/Loader.jsx";

export default function Contacts() {
  const { t, i18n } = useTranslation();
  const [aboutContacts, setAboutContacts] = useState([]);

  const [fetchContacts, isLoading, error] = useFetching(async () => {
    const response = await ContactsPageService.GetContacts(
      i18n.resolvedLanguage?.split("-")[0].toLowerCase()
    );
    if (response && Array.isArray(response.data)) {
      setAboutContacts(response.data);
    }
  });

  useEffect(() => {
    fetchContacts();
    // eslint-disable-next-line
  }, [i18n.language]);

  return (
    <div>
      {error && <p style={{ color: "red" }}>{error}</p>}
      {isLoading ? (
        <Loader />
      ) : (
        <div>
          <h3>{t("Contacts")}</h3>
          <ul>
            {aboutContacts.map((item) => (
              <li key={item.id}>
                <h3>{item.address}</h3>
                <h3>{item.phone}</h3>
                <h3>{item.email}</h3>
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}
