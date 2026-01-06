import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import LanguageDetector from 'i18next-browser-languagedetector';
import HttpBackend from 'i18next-http-backend';

i18n
  .use(HttpBackend) // JSON sub-clusion
  .use(LanguageDetector) // Detecting the user's language
  .use(initReactI18next) // Integration with React
  .init({
    fallbackLng: 'en', // Default language
    debug: true, // Disable in production

    interpolation: {
      escapeValue: true, // Disabling shielding
    },

    detection: {
      order: ['localStorage', 'cookie', 'navigator'], // Where to look for language
      caches: ['localStorage', 'cookie'], // Cache the language
    },

    backend: {
      loadPath: '/locales/{{lng}}/translation.json', // Where are translations stored
    },
  });

export default i18n;
