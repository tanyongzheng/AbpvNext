$full = $args[0]

# ����·�� 

$rootFolder = (Get-Item -Path "./" -Verbose).FullName

# ����ģʽ�µĽ�������б���Ҫ���������Ŀ�Ͳ���������Ŀ��.sln�ļ�����·��
$solutionPaths = @(
		"../templates",
		"../samples/WebApiDemo"
	)

if ($full -eq "-f")
{
	# ����������Ҫ���������������Ʃ��WPF��Ŀ
	$solutionPaths += (
		"../samples/ConsoleDemo"
	) 
}else{ 
	Write-host ""
	Write-host ":::::::::::::: !!! You are in development mode !!! ::::::::::::::" -ForegroundColor red -BackgroundColor  yellow
	Write-host "" 
} 
