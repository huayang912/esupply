package com.faurecia.service;

import java.util.List;

import com.faurecia.model.NoticeReader;

public interface NoticeReaderManager extends GenericManager<NoticeReader, Integer> {
	List<NoticeReader> getNoticeReaderByNoticeId(Integer noticeId);	
	List<NoticeReader> getReadNoticeReaderByNoticeId(Integer noticeId);
	NoticeReader getNoticeReaderByNoticeIdAndPlantSupplierId(Integer noticeId, Integer plantSupplierId);
	void deleteNoticeReaderByNoticeId(Integer noticeId);
}
