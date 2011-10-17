<%@ include file="/common/taglibs.jsp"%>

<s:form id="uploadForm" action="autoUpload" method="post"
	validate="true">
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc">Local File Folder</label></td>
			<td colspan="2"><s:textfield key="localFileFolder"
				cssClass="text medium" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc">Local File Backup Folder</label></td>
			<td colspan="2"><s:textfield key="localbackupFolder"
				cssClass="text medium" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc">Server File Folder</label></td>
			<td colspan="2"><s:textfield key="serverFileFolder"
				cssClass="text medium" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc">File Name</label></td>
			<td colspan="2"><s:textfield key="fileName"
				cssClass="text medium" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc">File Content</label></td>
			<td colspan="2"><s:textarea key="fileContent" cols="40"
				rows="10" cssClass="text medium" theme="simple" /></td>
		</tr>
		<tr>
			<td><s:submit cssClass="button" key="button.upload"
				theme="simple" /></td>
		</tr>
	</table>
	</li>
	<li class="buttonBar bottom"><input type="button" id="startUpload"
		onclick="document.getElementById('startUpload').disabled=true;document.getElementById('stopUpload').disabled = false;setTimeout('autoUpload()', 10000);"
		value="Start Upload" /> <input type="button" id="stopUpload"
		onclick="document.getElementById('uploadForm_button_upload').click();"
		value="Stop Upload" disabled /></li>

</s:form>
<script type="text/javascript">
function autoUpload() {
	var fso = new ActiveXObject("Scripting.FileSystemObject");
	var f = fso.GetFolder(document.getElementById("uploadForm_localFileFolder").value);	
	var fc = new Enumerator(f.files);
	if (!fc.atEnd())
  	{  	  	
  		var fileName = fso.GetFileName(fc.item());
  		var backupFileFullPath = document.getElementById("uploadForm_localbackupFolder").value + "\\" + fileName;
  		if (fso.FileExists(backupFileFullPath))
  		{
  			fso.DeleteFile(backupFileFullPath);
  		}
  		fso.MoveFile(fc.item(), backupFileFullPath); 
  		setTimeout(function(){readTextFile(fileName, backupFileFullPath);}, 1000);
    }
    else
    {
    	setTimeout('autoUpload()', 10000);
    }
}

function readTextFile(fileName, fileFullPath) {
	var fso = new ActiveXObject("Scripting.FileSystemObject");
		var objTextStream = fso.OpenTextFile(fileFullPath, 1);
  	var textContent = objTextStream.ReadAll();
  	objTextStream.Close();
  	setTimeout(function(){fillFileName(fileName, textContent);}, 1000);				
}


function fillFileName(fileName, textContent) {
		document.getElementById("uploadForm_fileName").value = fileName;
		setTimeout(function(){fillFileContent(textContent);}, 1000);
}

function fillFileContent(textContent) {
		document.getElementById("uploadForm_fileContent").value = textContent;
		setTimeout("submitForm();", 1000);
}

function submitForm() {
	document.getElementById('uploadForm_button_upload').click();
}

if ("${uploadSuccess}" == "true") {
	document.getElementById('startUpload').click();
}
</script>