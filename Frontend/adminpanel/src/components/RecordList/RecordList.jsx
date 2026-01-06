import React, { useContext, forwardRef } from 'react';
import { AnimatePresence, motion } from 'framer-motion';
import { ModalContext } from '../../provider/context/ModalProvider.jsx';
import { itemVariants } from '../../utils/animationVariants.js';
import RecordItem from '../RecordItem/RecordItem.jsx';

const MemoizedRecordItem = React.memo(
  forwardRef((props, ref) => <RecordItem ref={ref} {...props} />)
);

export default function RecordList({ records, token }) {
  let { currentTable } = useContext(ModalContext);

  if (currentTable === '') {
    currentTable = 'Database Records';
  }

  if (!records || records.length === 0) {
    return (
      <h1 style={{ textAlign: 'center' }}>
        {currentTable} table has no records!
      </h1>
    );
  }

  return (
    <div>
      <h1 className="listHeader">{currentTable}</h1>
      <AnimatePresence>
        {records.map((record) => (
          <motion.div
            key={record.id}
            variants={itemVariants}
            initial="hidden"
            animate="visible"
            exit="exit"
            layout
          >
            <MemoizedRecordItem record={record} token={token} />
          </motion.div>
        ))}
      </AnimatePresence>
    </div>
  );
}
