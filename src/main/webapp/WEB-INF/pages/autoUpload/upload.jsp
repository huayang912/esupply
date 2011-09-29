<%@ include file="/common/taglibs.jsp"%>

<s:form id="uploadForm" action="autoUpload"
	enctype="multipart/form-data" method="post" validate="true">
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
			<td colspan="3"><s:file name="file"
				label="%{getText('uploadForm.file')}" cssClass="text file" /></td>
				<td><s:submit cssClass="button" key="button.upload"
				theme="simple" /></td>
		</tr>
	</table>
	</li>
	<li class="buttonBar bottom">
		<input type="button" id="startUpload"
		onclick="document.getElementById('startUpload').disabled=true;document.getElementById('stopUpload').disabled = false;setTimeout('autoUpload()', 10000);" value="Start Upload" />
		<input type="button" id="stopUpload"
		onclick="document.getElementById('uploadForm_button_upload').click();" value="Stop Upload" disabled /></li>

</s:form>
<script type="text/javascript">
function autoUpload() {
	var fso = new ActiveXObject("Scripting.FileSystemObject");
	var f = fso.GetFolder(document.getElementById("uploadForm_LocalFileFolder").value);
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
  		setTimeout(function(){fillUploadBox(backupFileFullPath);}, 1000);					
    }
    
    setTimeout('autoUpload()', 10000);
}

function fillUploadBox(filePath) {
	var file_input_obj = document.getElementById("uploadForm_file");
	file_input_obj.focus();
	var WshShell=new ActiveXObject("WScript.Shell");
	WshShell.sendKeys(filePath);			
	setTimeout("submitUploadForm()", 1000);
}

function submitUploadForm() {
	document.getElementById("uploadForm_button_upload").focus();	
	var WshShell=new ActiveXObject("WScript.Shell");
	WshShell.sendKeys("{Enter}");
}

if ("${uploadSuccess}" == "true") {
	document.getElementById('startUpload').click();
}
</script>