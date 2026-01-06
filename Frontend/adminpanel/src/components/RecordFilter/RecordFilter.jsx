import React from 'react';
import GeneralInput from '../UI/input/GeneralInput.jsx';
import GeneralSelect from '../UI/select/GeneralSelect.jsx';

export default function RecordFilter({ filter, setFilter, fields }) {
  const options = [];

  for (var i = 0; i < fields.length; i++) {
    options.push({
      name: fields[i],
      value: fields[i],
    });
  }

  return (
    <div>
      <h4>Choose field for search and sort:</h4>
      <GeneralSelect
        value={filter.sort}
        onChange={(selectedSort) =>
          setFilter({ ...filter, sort: selectedSort })
        }
        defultValue="Sort by"
        options={options}
      />
      <GeneralInput
        placeholder="Search"
        value={filter.query}
        onChange={(e) => setFilter({ ...filter, query: e.target.value })}
      />
    </div>
  );
}
