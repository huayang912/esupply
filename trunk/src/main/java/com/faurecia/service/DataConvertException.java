package com.faurecia.service;

public class DataConvertException extends Exception {
	/**
	 * 
	 */
	private static final long serialVersionUID = -245929501991163528L;

	private Object obj;
	
	public DataConvertException(final Exception ex, final Object obj) {
		super(ex);
		this.obj = obj;
	}
	
	public Object getObject() {
		return obj;
	}
}
