(function () {


    let vocabularyId = document.getElementById('vocabularyId')?.value;

    //let vocabularyIdInput = document.getElementById('vocabularyId')?.value;
    //if (vocabularyIdInput) {
    //    vocabularyId = vocabularyIdInput;
    //}


    let audio = new Audio();

    let availableWordsArea = document.getElementById('available-words');

    document.addEventListener('click', function (evt) {
        let targetElement = evt.target;
       

        if (targetElement.classList.contains('show-translations-btn')) {
            loadTranslationList(targetElement);
            return;
        }

        if (targetElement.classList.contains('switch-own-word-btn')) {
            switchOwnWordButton(targetElement);
            return;
        }

        if (targetElement.classList.contains('add-word-btn')) {
            addWord(targetElement);
            return;
        }

        if (targetElement.classList.contains('add-own-word-btn')) {
            addOwnWord(targetElement);
            return;
        }

        if (targetElement.classList.contains('audio'))
        {
            playAudio(targetElement);
            return;
        }


        if (targetElement.classList.contains('delete-button'))
        {
            deleteWord(targetElement);
            return;
        }



        do {
            if (targetElement === availableWordsArea) return;
            targetElement = targetElement.parentNode;
        } while (targetElement);

        availableWordsArea.innerHTML = '';

        availableWordsArea.classList.add('d-none');

    });

    let input = document.querySelector("#find-input");

    input.addEventListener("input", function () {
        toogleButton(this.value);
        refreshArea(this.value, this.dataset.requestUrl);
    }, false);

   


    function switchOwnWordButton(btn) {
        var template = document.getElementById('addOwnWordTemplate').cloneNode(true);
        btn.parentElement.replaceChild(template.content, btn);
    }

    function addWord(button) {
        $.ajax({
            url: button.dataset.requestUrl,
            type: 'POST',
            data: { word: input.value, translation: button.innerText, vocabularyId: vocabularyId },
            success: function (res) {
                location.reload();
            }
        });
    }

    function addOwnWord(button) {
        let translation = button.previousElementSibling.value;
        $.ajax({
            url: button.dataset.requestUrl,
            type: 'POST',
            data: { word: input.value, translation: translation, vocabularyId: vocabularyId },
            success: function (res) {
                location.reload();
            }
        });
    }

    function loadTranslationList(button) {
        availableWordsArea.classList.remove('d-none');
        $.ajax({
            url: button.dataset.requestUrl,
            type: 'GET',
            data: { word: input.value },
            success: (res) => {
                var area = document.querySelector('#available-words');
                area.innerHTML = res;
            }
        });
    }

    function toogleButton(str) {
        let button = document.querySelector(".show-translations-btn");
        if (str.length > 0) {
            button.style.visibility = "visible";
        } else {
            button.style.visibility = "hidden";
        }
    }

    function refreshArea(word, url) {
        $.ajax({
            url: url,
            type: 'GET',
            data: { mask: word, vocabularyId: vocabularyId },
            success: (res) => {
                var refreshArea = document.querySelector("#wordList");
                refreshArea.innerHTML = res;
            }
        });
    }

    function playAudio(button) {

        if (button.nodeName === "I") {
            button = button.parentElement;
        }

        let lang = button.classList.contains('translation') ? 2 : 1;
        $.ajax({
            url: button.dataset.requestUrl,
            type: 'GET',
            data: { word: button.previousElementSibling.innerText, language: lang },
            success: function (res) {
                audio.src = '';
                audio.src = res;
                audio.play();
            }
        });
    }

    function deleteWord(button) {
        let wordId = button.parentElement.parentElement.querySelector('input[name="wordId"]').value;
        $.ajax({
            url: button.dataset.requestUrl,
            type: 'POST',
            data: { wordId: wordId, vocabularyId: vocabularyId },
            success: function (res) {
                location.reload();
                //todo убрать из dom модели tr без reload
            }
        });
    }
})();