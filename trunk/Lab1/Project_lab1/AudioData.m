classdef AudioData
    %UNTITLED Summary of this class goes here
    %   Detailed explanation goes here   
    properties
        audio
    end
    properties
        meanAudio
    end
     properties
        varAudio
    end
    properties
        pathFile
    end
    properties
        probability
    end
    properties
        label
    end
    methods       
        function obj = AudioData(path, labelFile)
            obj.pathFile = path;
            a = wavread(path);
            obj.audio = mfcc(a);
            obj.meanAudio = mean(obj.audio);
            obj.varAudio = var(obj.audio);
            obj.label = labelFile;
        end
        
        function x = calculateProbability(obj,sample)
            a = wavread(sample);
            a = mfcc(a);
            %p = mean(mvnpdf(a,mean(a),var(a)));         
            %obj.probability = mean(mvnpdf(a, mean(obj.audio), var(obj.audio)));
            x = mean(mvnpdf(a, obj.meanAudio, obj.varAudio));            
        end
                
    end
    
end

