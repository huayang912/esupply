package com.faurecia.util;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.SocketException;
import java.util.ArrayList;
import java.util.List;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.apache.commons.net.ftp.FTP;
import org.apache.commons.net.ftp.FTPClient;
import org.apache.commons.net.ftp.FTPFile;

public class FTPClientUtil {

	private FTPClient ftpClient;
	protected transient final Log log = LogFactory.getLog(getClass());
	public static final int BINARY_FILE_TYPE = FTP.BINARY_FILE_TYPE;
	public static final int ASCII_FILE_TYPE = FTP.ASCII_FILE_TYPE;
	public static final String SEPERATE = "/";

	// path should not the path from root index
	// or some FTP server would go to root as '/'.
	public void connectServer(String server, int port, String user,
			String password, String path) throws SocketException, IOException {
		ftpClient = new FTPClient();
		// ftpClient.setControlEncoding("GBK"); //解决中文文件名乱码问题
		ftpClient.connect(server, port);
		log.info("Connected to " + server + ".");
		log.info(ftpClient.getReplyCode());
		ftpClient.login(user, password);
		// Path is the sub-path of the FTP path
		if (path.length() != 0) {
			ftpClient.changeWorkingDirectory(path);
		}
	}

	// FTP.BINARY_FILE_TYPE | FTP.ASCII_FILE_TYPE
	// Set transform type
	public void setFileType(int fileType) throws IOException {
		ftpClient.setFileType(fileType);
	}

	public void closeServer() throws IOException {
		if (ftpClient.isConnected()) {
			ftpClient.disconnect();
		}
	}

	// =======================================================================
	// == About directory =====
	// The following method using relative path better.
	// =======================================================================

	public boolean changeDirectory(String path) throws IOException {
		return ftpClient.changeWorkingDirectory(path);
	}

	public boolean createDirectory(String pathName) throws IOException {
		return ftpClient.makeDirectory(pathName);
	}

	public boolean removeDirectory(String path) throws IOException {
		return ftpClient.removeDirectory(path);
	}

	// delete all subDirectory and files.
	public boolean removeDirectory(String path, boolean isAll)
			throws IOException {

		if (!isAll) {
			return removeDirectory(path);
		}

		FTPFile[] ftpFileArr = ftpClient.listFiles(path);
		if (ftpFileArr == null || ftpFileArr.length == 0) {
			return removeDirectory(path);
		}
		//    
		for (FTPFile ftpFile : ftpFileArr) {
			String name = ftpFile.getName();
			if (ftpFile.isDirectory()) {
				if (!ftpFile.getName().equals(".")    //去掉.和..，不然就是死循环了
						&& (!ftpFile.getName().equals(".."))) {
					log.debug("* [sD]Delete subPath [" + path + "/" + name + "]");
					removeDirectory(path + "/" + name, true);
				}
			} else if (ftpFile.isFile()) {
				log.debug("* [sF]Delete file [" + path + "/" + name + "]");
				deleteFile(path + "/" + name);
			} else if (ftpFile.isSymbolicLink()) {

			} else if (ftpFile.isUnknown()) {

			}
		}
		return ftpClient.removeDirectory(path);
	}

	// Check the path is exist; exist return true, else false.
	public boolean existDirectory(String path) throws IOException {
		boolean flag = false;
		FTPFile[] ftpFileArr = ftpClient.listFiles(path);
		for (FTPFile ftpFile : ftpFileArr) {
			if (ftpFile.isDirectory()
					&& ftpFile.getName().equalsIgnoreCase(path)) {
				flag = true;
				break;
			}
		}
		return flag;
	}

	// =======================================================================
	// == About file =====
	// Download and Upload file using
	// ftpUtil.setFileType(FtpUtil.BINARY_FILE_TYPE) better!
	// =======================================================================

	// #1. list & delete operation
	// Not contains directory
	public List<String> getFileList(String path) throws IOException {
		// listFiles return contains directory and file, it's FTPFile instance
		// listNames() contains directory, so using following to filer
		// directory.
		// String[] fileNameArr = ftpClient.listNames(path);
		FTPFile[] ftpFiles = ftpClient.listFiles(path);

		List<String> retList = new ArrayList<String>();
		if (ftpFiles == null || ftpFiles.length == 0) {
			return retList;
		}
		for (FTPFile ftpFile : ftpFiles) {
			if (ftpFile.isFile()) {
				retList.add(ftpFile.getName());
			}
		}
		return retList;
	}

	public boolean deleteFile(String pathName) throws IOException {
		return ftpClient.deleteFile(pathName);
	}

	// #2. upload to ftp server
	// InputStream <------> byte[] simple and See API

	public boolean uploadFile(String fileName, String newName)
			throws IOException {
		boolean flag = false;
		InputStream iStream = null;
		try {
			iStream = new FileInputStream(fileName);
			flag = ftpClient.storeFile(newName, iStream);
		} catch (IOException e) {
			flag = false;
			return flag;
		} finally {
			if (iStream != null) {
				iStream.close();
			}
		}
		return flag;
	}

	public boolean uploadFile(String fileName) throws IOException {
		return uploadFile(fileName, fileName);
	}

	public boolean uploadFile(InputStream iStream, String newName)
			throws IOException {
		boolean flag = false;
		try {
			// can execute [OutputStream storeFileStream(String remote)]
			// Above method return's value is the local file stream.
			flag = ftpClient.storeFile(newName, iStream);
		} catch (IOException e) {
			flag = false;
			return flag;
		} finally {
			if (iStream != null) {
				iStream.close();
			}
		}
		return flag;
	}

	// #3. Down load

	public boolean download(String remoteFileName, String localFileName)
			throws IOException {
		boolean flag = false;
		File outfile = new File(localFileName);
		OutputStream oStream = null;
		try {
			oStream = new FileOutputStream(outfile);
			flag = ftpClient.retrieveFile(remoteFileName, oStream);
		} catch (IOException e) {
			flag = false;
			return flag;
		} finally {
			oStream.close();
		}
		return flag;
	}

	public InputStream downFile(String sourceFileName) throws IOException {
		return ftpClient.retrieveFileStream(sourceFileName);
	}
}
