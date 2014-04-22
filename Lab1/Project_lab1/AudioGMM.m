classdef AudioGMM
    %UNTITLED Summary of this class goes here
    %   Detailed explanation goes here
    
    properties
        label
        audio
        g2        
    end
    properties (Constant)
        numberOfGauss=16
        numberOfLoop=100
    end
    
    methods
        function obj = AudioGMM(pathFile, label, numLoop, numGauss)
            obj.label = label;
            obj.audio = wavread(pathFile);
            obj.audio = mfcc(obj.audio);
            g0 = gNew(12, numGauss, 'diag');
            g1 = gInit(g0,obj.audio, numLoop);
            obj.g2 = gRE(g1,obj.audio, numLoop);
        end
        
        function x = calculate(obj,pathSample)
            sampleAudio = wavread(pathSample);
            sampleAudio = mfcc(sampleAudio);
            x = mean(gPr(obj.g2, sampleAudio));
        end
    end
    
end

