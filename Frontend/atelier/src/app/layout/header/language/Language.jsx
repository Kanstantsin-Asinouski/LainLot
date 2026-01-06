import React from 'react';
import { useTranslation } from 'react-i18next';
import mcss from './Language.module.css';

export default function Language() {
  const { i18n } = useTranslation();

  const languages = {
    en: {
      label: 'English',
      flag: 'images/uk_16x16.png',
      title: 'United Kingdom',
    },
    ru: { label: 'Русский', flag: 'images/russia_16x16.png', title: 'Россия' },
    pl: { label: 'Polska', flag: 'images/poland_16x16.png', title: 'Polska' },
  };

  const currentLang = languages[i18n.language] || languages.en;

  return (
    <div className={mcss.lang_box}>
      <button
        className="nav-link dropdown-toggle"
        data-toggle="dropdown"
        aria-expanded="true"
      >
        <img
          src={currentLang.flag}
          alt="flag"
          className="mr-2"
          title={currentLang.title}
        />
        {currentLang.label}
        <i className="fa fa-angle-down ml-2" aria-hidden="true"></i>
      </button>
      <div className="dropdown-menu">
        {Object.keys(languages).map((lang) => (
          <button
            key={lang}
            className="dropdown-item"
            onClick={() => i18n.changeLanguage(lang)}
          >
            <img src={languages[lang].flag} className="mr-2" alt="flag" />
            {languages[lang].label}
          </button>
        ))}
      </div>
    </div>
  );
}
