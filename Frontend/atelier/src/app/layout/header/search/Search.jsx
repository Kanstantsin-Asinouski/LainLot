import React from 'react';
import { useTranslation } from 'react-i18next';
import mcss from './Search.module.css';

export default function Search() {
  const { t } = useTranslation();

  return (
    <div className={mcss.main}>
      <div className="input-group">
        <input
          type="text"
          className="form-control"
          placeholder={t('SearchThisSite')}
        />
        <div className="input-group-append">
          <button className="btn btn-secondary btn-search" type="button">
            <i className="fa fa-search"></i>
          </button>
        </div>
      </div>
    </div>
  );
}
