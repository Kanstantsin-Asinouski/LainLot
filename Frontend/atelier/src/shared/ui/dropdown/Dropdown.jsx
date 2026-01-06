import React from 'react';
import { useTranslation } from 'react-i18next';

export default function Dropdown() {
  const { t } = useTranslation();

  return (
    <div className="dropdown">
      <button
        className="btn btn-secondary dropdown-toggle"
        type="button"
        id="dropdownMenuButton"
        data-toggle="dropdown"
        aria-haspopup="true"
        aria-expanded="false"
      >
        {t('AllCategory')}
      </button>
      <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
        <a className="dropdown-item" href="/home">
          Action
        </a>
        <a className="dropdown-item" href="/home">
          Another action
        </a>
        <a className="dropdown-item" href="/home">
          Something else here
        </a>
      </div>
    </div>
  );
}
