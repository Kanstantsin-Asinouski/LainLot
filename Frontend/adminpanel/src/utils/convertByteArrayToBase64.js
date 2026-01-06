export const byteArrayToBase64 = (byteArray) => {
  if (byteArray instanceof Array || byteArray instanceof Uint8Array) {
    const base64String = btoa(
      byteArray.reduce((data, byte) => data + String.fromCharCode(byte), '')
    );
    return base64String;
  } else if (typeof byteArray === 'string') {
    return byteArray;
  } else {
    console.error('Provided data is not a valid byteArray:', byteArray);
    return null;
  }
};
