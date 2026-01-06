import React, { useEffect, useState, useContext, useCallback } from 'react';
import secureLocalStorage from 'react-secure-storage';
import { useParams, useNavigate } from 'react-router-dom';
import { useFetching } from '../hooks/useFetching.jsx';
import { getRecordById } from '../utils/getRecordById.js';
import { byteArrayToBase64 } from '../utils/convertByteArrayToBase64.js';
import { DataContext } from '../provider/context/DataProvider.jsx';
import { ModalContext } from '../provider/context/ModalProvider.jsx';
import { ForeignKeysContext } from '../provider/context/ForeignKeysProvider.jsx';
import { PaginationContext } from '../provider/context/PaginationProvider.jsx';
import Loader from '../components/UI/loader/Loader.jsx';
import GeneralButton from '../components/UI/button/GeneralButton.jsx';
import GeneralModal from '../components/UI/modal/GeneralModal.jsx';
import RecordForm from '../components/RecordForm/RecordForm.jsx';
import DisplayImage from '../components/UI/image/DisplayImage.jsx';

export default function RecordIdPage() {
  const { modal, setModal, fetchRecords } = useContext(ModalContext);
  const { page, limit } = useContext(PaginationContext);
  const { openEditModal, removeRecord } = useContext(ModalContext);
  const { setCurrentTable, currentTable, currentRecords } =
    useContext(DataContext);
  const { fetchMultipleFkData, foreignKeys, fkError } =
    useContext(ForeignKeysContext);

  const token = secureLocalStorage.getItem('token');

  const params = useParams();
  const navigate = useNavigate();
  const [record, setRecord] = useState({});

  const [fetchRecordById, isLoading, error] = useFetching(async (table, id) => {
    const response = await getRecordById(table, id, token);
    if (response?.data) {
      setRecord(response.data);
    }
  });

  const handleReturnToRecords = useCallback(() => {
    navigate('/records');
  }, [navigate]);

  const handleOpenEditModal = useCallback(() => {
    openEditModal(record);
  }, [openEditModal, record]);

  const handleRemoveRecord = useCallback(() => {
    removeRecord(record, token);
    navigate('/records');
  }, [removeRecord, record, navigate, token]);

  useEffect(() => {
    setCurrentTable(params.table);
    fetchRecords(limit, page, token);
    fetchRecordById(params.table, params.id);
    // eslint-disable-next-line
  }, [currentTable]);

  useEffect(() => {
    fetchRecordById(params.table, params.id);
    // eslint-disable-next-line
  }, [currentRecords]);

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
    <div>
      <GeneralModal visible={modal} setVisible={setModal}>
        <RecordForm token={token} />
      </GeneralModal>
      <h1>
        {params.table} page with id {params.id}
      </h1>
      {error !== null ? (
        isLoading ? (
          <Loader />
        ) : (
          <div>
            {Object.entries(record).map(([key, value]) => (
              <div key={key}>
                {key}:{' '}
                {key.startsWith('fk') ? (
                  fkError ? (
                    fkError
                  ) : (
                    `${foreignKeys[`${key}_${value}`] || 'Loading...'} (${value})`
                  )
                ) : key === 'imageData' ? (
                  <DisplayImage
                    base64Img={byteArrayToBase64(value)}
                    fullSize={false}
                  />
                ) : (
                  value
                )}
              </div>
            ))}
          </div>
        )
      ) : (
        <h1>{error.message}</h1>
      )}
      <div>
        <GeneralButton onClick={handleReturnToRecords}>
          Return to {params.table}
        </GeneralButton>
        <GeneralButton onClick={handleOpenEditModal}>Edit</GeneralButton>
        <GeneralButton onClick={handleRemoveRecord}>Delete</GeneralButton>
      </div>
    </div>
  );
}
