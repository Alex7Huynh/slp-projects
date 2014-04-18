function varargout = Exe1(varargin)
% EXE1 MATLAB code for Exe1.fig
%      EXE1, by itself, creates a new EXE1 or raises the existing
%      singleton*.
%
%      H = EXE1 returns the handle to a new EXE1 or the handle to
%      the existing singleton*.
%
%      EXE1('CALLBACK',hObject,eventData,handles,...) calls the local
%      function named CALLBACK in EXE1.M with the given input arguments.
%
%      EXE1('Property','Value',...) creates a new EXE1 or raises the
%      existing singleton*.  Starting from the left, property value pairs are
%      applied to the GUI before Exe1_OpeningFcn gets called.  An
%      unrecognized property name or invalid value makes property application
%      stop.  All inputs are passed to Exe1_OpeningFcn via varargin.
%
%      *See GUI Options on GUIDE's Tools menu.  Choose "GUI allows only one
%      instance to run (singleton)".
%
% See also: GUIDE, GUIDATA, GUIHANDLES

% Edit the above text to modify the response to help Exe1

% Last Modified by GUIDE v2.5 17-Apr-2014 19:28:01

% Begin initialization code - DO NOT EDIT
gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
                   'gui_Singleton',  gui_Singleton, ...
                   'gui_OpeningFcn', @Exe1_OpeningFcn, ...
                   'gui_OutputFcn',  @Exe1_OutputFcn, ...
                   'gui_LayoutFcn',  [] , ...
                   'gui_Callback',   []);
if nargin && ischar(varargin{1})
    gui_State.gui_Callback = str2func(varargin{1});
end

if nargout
    [varargout{1:nargout}] = gui_mainfcn(gui_State, varargin{:});
else
    gui_mainfcn(gui_State, varargin{:});
end
% End initialization code - DO NOT EDIT


% --- Executes just before Exe1 is made visible.
function Exe1_OpeningFcn(hObject, eventdata, handles, varargin)
% This function has no output args, see OutputFcn.
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
% varargin   command line arguments to Exe1 (see VARARGIN)

% Choose default command line output for Exe1
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% UIWAIT makes Exe1 wait for user response (see UIRESUME)
% uiwait(handles.figure1);


% --- Outputs from this function are returned to the command line.
function varargout = Exe1_OutputFcn(hObject, eventdata, handles) 
% varargout  cell array for returning output args (see VARARGOUT);
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Get default command line output from handles structure
varargout{1} = handles.output;
set(handles.rdGauss, 'Value', 1);
set(handles.rdGMM, 'Value', 0);
set(handles.txtNumberLoop, 'Enable', 'off');
set(handles.txtNumberGauss, 'Enable', 'off');



function txtPathFile_Callback(hObject, eventdata, handles)
% hObject    handle to txtPathFile (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtPathFile as text
%        str2double(get(hObject,'String')) returns contents of txtPathFile as a double


% --- Executes during object creation, after setting all properties.
function txtPathFile_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtPathFile (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


% --- Executes on button press in btnChooseFile.
function btnChooseFile_Callback(hObject, eventdata, handles)
% hObject    handle to btnChooseFile (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)


[FileName,PathName,FilterIndex] = uigetfile({'*.wav'},'Please choose file');
FileName = strcat(PathName,'\',FileName );
set(handles.txtPathFile,'String', FileName);


% --- Executes on button press in btnTrain.
function btnTrain_Callback(hObject, eventdata, handles)
% hObject    handle to btnTrain (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
folder_name = uigetdir('C:\','Choose folder to train');
set(handles.txtTrainFolder,'String', folder_name);




function txtTrainFolder_Callback(hObject, eventdata, handles)
% hObject    handle to txtTrainFolder (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtTrainFolder as text
%        str2double(get(hObject,'String')) returns contents of txtTrainFolder as a double


% --- Executes during object creation, after setting all properties.
function txtTrainFolder_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtTrainFolder (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


% --- Executes on button press in btnDetect.
function btnDetect_Callback(hObject, eventdata, handles)
% hObject    handle to btnDetect (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

clear arrayAudio;

folderPath = get(handles.txtTrainFolder,'String');
samplePath = get(handles.txtPathFile, 'String');
matchedLabel = '1';
currentProbability = 0;
isGauss = get(handles.rdGauss, 'Value');
for (n = 1:2)
    tempF = strcat(folderPath, '\', int2str(n), '.wav');
    temp = 0;
    if (isGauss == 1)
        arrayAudio(n) = AudioData(tempF,int2str(n));
        temp = arrayAudio(n).calculateProbability(samplePath);
    end
    if (isGauss == 0)
        AudioGMM.numberOfLoop = int2str(get(handles.txtNumberLoop, 'String'));
        AudioGMM.numberOfGauss = int2str(get(handles.txtNumberGauss, 'String'));
        arrayAudio(n) = AudioGMM(tempF,int2str(n));
        temp = arrayAudio(n).calculate(samplePath);
    end  
    
    
    if (currentProbability <= temp)
        currentProbability = temp;
        matchedLabel = arrayAudio(n).label;
    end
end
h = msgbox(strcat('The label is  ',matchedLabel),'Result');


% --- Executes on button press in rdGauss.
function rdGauss_Callback(hObject, eventdata, handles)
% hObject    handle to rdGauss (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hint: get(hObject,'Value') returns toggle state of rdGauss
set(handles.rdGMM,'Value',0)
;set(handles.rdGauss,'Value',1);
set(handles.txtNumberLoop, 'Enable', 'off');
set(handles.txtNumberGauss, 'Enable', 'off');

% --- Executes on button press in rdGMM.
function rdGMM_Callback(hObject, eventdata, handles)
% hObject    handle to rdGMM (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hint: get(hObject,'Value') returns toggle state of rdGMM
set(handles.rdGMM,'Value',1);
set(handles.rdGauss,'Value',0);
set(handles.txtNumberLoop, 'Enable', 'on');
set(handles.txtNumberGauss, 'Enable', 'on');


function txtNumberGauss_Callback(hObject, eventdata, handles)
% hObject    handle to txtNumberGauss (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtNumberGauss as text
%        str2double(get(hObject,'String')) returns contents of txtNumberGauss as a double


% --- Executes during object creation, after setting all properties.
function txtNumberGauss_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtNumberGauss (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function txtNumberLoop_Callback(hObject, eventdata, handles)
% hObject    handle to txtNumberLoop (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of txtNumberLoop as text
%        str2double(get(hObject,'String')) returns contents of txtNumberLoop as a double


% --- Executes during object creation, after setting all properties.
function txtNumberLoop_CreateFcn(hObject, eventdata, handles)
% hObject    handle to txtNumberLoop (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end
