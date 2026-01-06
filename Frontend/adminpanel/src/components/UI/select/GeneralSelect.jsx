import React from 'react';
import mcss from './GeneralSelect.module.css';

export default function GeneralSelect({
  options,
  defaultValue,
  value,
  onChange,
}) {
  return (
    <select
      className={mcss.generalSelect}
      value={value}
      onChange={(e) => onChange(e.target.value)}
    >
      <option disabled value="">
        {defaultValue}
      </option>
      {options.map((option) => (
        <option key={option.value} value={option.value}>
          {option.name}
        </option>
      ))}
    </select>
  );
}
