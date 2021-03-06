﻿document.querySelector('button.record').addEventListener('click', function () {
    const constraints = {
        audio: {
            sampleRate: 48000,
            channelCount: 1,
            volume: 1.0,
            echoCancellation: true,
            noiseSuppression: true,
        },
        video: false
    };

    navigator.mediaDevices.getUserMedia(constraints)
        .then(stream => {
            let options = window.MediaRecorder.isTypeSupported('audio/webm') ? {
                mimeType: 'audio/webm'
            } : {};

            const mediaRecorder = new window.MediaRecorder(stream, options);
            mediaRecorder.start();
            const audioChunks = [];
            mediaRecorder.addEventListener("dataavailable", event => {
                audioChunks.push(event.data);
            });

            mediaRecorder.addEventListener("stop", () => {
                const audioBlob = new Blob(audioChunks);
                const audioUrl = URL.createObjectURL(audioBlob);


                const downloadLink = document.getElementById('download');
                downloadLink.href = audioUrl;
                downloadLink.download = 'acetest.wav';
                const audio = new Audio(audioUrl);
                audio.play();
                var fd = new FormData();
                fd.append('fname', 'test.wav');
                fd.append('formFile', audioBlob);
                $.ajax({
                    type: 'POST',
                    url: '@Url.Action("GetText", "Home")',
                    data: fd,
                    processData: false,
                    contentType: false
                }).done(function (data) {
                    document.getElementById('word').innerHTML = data;
                });
            });

            setTimeout(() => {
                mediaRecorder.stop();

            }, 3000);
        });
});