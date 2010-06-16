package com.faurecia.service;

import java.util.List;

import com.faurecia.model.NoticeReader;

public interface NoticeReaderManager extends GenericManager<NoticeReader, Integer> {
	List<NoticeReader> getNoticeReaderByNoticeId(Integer noticeId);	
	
	void deleteNoticeReaderByNoticeId(Integer noticeId);
}
