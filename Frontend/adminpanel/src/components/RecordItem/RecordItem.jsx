import { useContext, useCallback, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { ModalContext } from '../../provider/context/ModalProvider.jsx';
import { ForeignKeysContext } from '../../provider/context/ForeignKeysProvider.jsx';
import { byteArrayToBase64 } from '../../utils/convertByteArrayToBase64.js';
import GeneralButton from '../UI/button/GeneralButton.jsx';
import DisplayImage from '../UI/image/DisplayImage.jsx';
import mcss from './RecordItem.module.css';

export default function RecordItem({ record, ref, token }) {
  const { openEditModal, removeRecord, currentTable } =
    useContext(ModalContext);
  const { fetchMultipleFkData, foreignKeys, fkError } =
    useContext(ForeignKeysContext);

  const navigate = useNavigate();

  const handleOpenRecordIdPage = useCallback(() => {
    navigate(`/records/${currentTable}/${record.id}`);
  }, [navigate, currentTable, record.id]);

  const handleOpenEditModal = useCallback(() => {
    openEditModal(record);
  }, [openEditModal, record]);

  const handleRemoveRecord = useCallback(() => {
    removeRecord(record, token);
  }, [removeRecord, record, token]);

  useEffect(() => {
    const fkFields = Object.entries(record)
      .filter(([key]) => key.startsWith('fk'))
      .map(([key, value]) => ({ key, value }));

    if (fkFields.length) {
      fetchMultipleFkData(fkFields, token);
    }
    // eslint-disable-next-line
  }, [record, fetchMultipleFkData]);

  return (
    <div className={mcss.post} ref={ref}>
      <div className={mcss.postContent}>
        {Object.keys(record).map((key) => {
          const foreignKeyData = foreignKeys[`${key}_${record[key]}`];

          return (
            <div key={key} className={mcss.recordRow}>
              <span className={mcss.recordKey}>{key}:</span>
              {key.startsWith('fk') ? (
                <div className={mcss.foreignKeyWrapper}>
                  {fkError ? (
                    <span className={mcss.errorText}>{fkError}</span>
                  ) : (
                    <>
                      <span className={mcss.foreignKeyId}>
                        ID: {record[key]}
                      </span>
                      <div className={mcss.foreignKeyValue}>
                        {foreignKeyData ? (
                          <pre>{JSON.stringify(foreignKeyData, null, 2)}</pre>
                        ) : (
                          <span>Loading...</span>
                        )}
                      </div>
                    </>
                  )}
                </div>
              ) : key === 'imageData' ? (
                <DisplayImage
                  base64Img={byteArrayToBase64(record[key])}
                  fullSize={false}
                />
              ) : (
                <span className={mcss.recordValue}>{record[key]}</span>
              )}
            </div>
          );
        })}
      </div>
      <div className={mcss.postBtns}>
        <GeneralButton onClick={handleOpenRecordIdPage}>Open</GeneralButton>
        <GeneralButton onClick={handleOpenEditModal}>Edit</GeneralButton>
        <GeneralButton onClick={handleRemoveRecord}>Delete</GeneralButton>
      </div>
    </div>
  );
}
