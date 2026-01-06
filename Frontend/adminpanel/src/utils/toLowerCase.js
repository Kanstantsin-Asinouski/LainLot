export const toLowerCase = (fields) => {
  var lowerFields = [];

  for (var i = 0; i < fields.length; i++) {
    var word = fields[i].charAt(0).toLowerCase() + fields[i].slice(1);
    lowerFields.push(word);
  }

  return lowerFields;
};
