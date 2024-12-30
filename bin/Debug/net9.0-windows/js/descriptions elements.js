function delay(ms)
{
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function insertText(requirements, addiptionalOptions, about)
{
    document.getElementById('cke_400').click();
    document.getElementById('cke_704').click();
    document.getElementById('cke_1388').click();

    await delay(1000);

    let textareaRequirements = document.querySelector('textarea[title="Rich Text Editor, id_description_elements-1-content"]');
    let textareaAdditionalOptions = document.querySelector('textarea[title="Rich Text Editor, id_description_elements-5-content"]');
    let textareaAbout = document.querySelector('textarea[title="Rich Text Editor, id_description_elements-14-content"]');

    if (requirements != "")
        textareaRequirements.value = requirements;
    if (addiptionalOptions != "")
        textareaAdditionalOptions.value = addiptionalOptions;
    if (about != "")
        textareaAbout.value = about;
    
    document.getElementById('cke_400').click();
    document.getElementById('cke_704').click();
    document.getElementById('cke_1388').click();
}

function changeSelectElement(trigger, block)
{
    if (block == "")
        return;

    var selectElement = document.getElementById(trigger);

    if (block == 0) { selectElement.value = selectElement.options[0].value; }
    else if (block == 1) { selectElement.value = "blocks_one"; }
    else if (block == 2) { selectElement.value = "blocks_two"; }

    var event = new Event('change', { bubbles: true });
    selectElement.dispatchEvent(event);
}

(function interceptWindowOpen()
{
    const originalOpen = window.open;
    window.open = function(...args) 
    {    
        const newWin = originalOpen.apply(this, args);
        window._popupRef = newWin;
        return newWin;
    };
})();

async function executeFunction(trigger, position, title, body)
{
    while(!_status)
    {
        await delay(100);
    }

    _status = false;

    document.querySelector(trigger).click();
    await delay(2000);

    window._popupRef.document.querySelector('input[name="pos"]').value = position;
    window._popupRef.document.querySelector('input[name="title"]').value = title;
    window._popupRef.document.getElementById('cke_19').click();
    await delay(500);

    window._popupRef.document.querySelector('textarea[title="Rich Text Editor, id_content"]').value = body;
    window._popupRef.document.getElementById('cke_19').click();
    await delay(500);

    window._popupRef.document.querySelector('input[type="submit"][value="Save"]').click();
    await delay(500);
}

function insertStaticText(aboutTitle)
{
    document.getElementById("id_description_elements-0-title").value = "Requirements";

    document.getElementById("id_description_elements-1-show_title").checked = true;
    document.getElementById("id_description_elements-1-title").value = "Requirements";

    document.getElementById("id_description_elements-2-show_title").checked = false;

    document.getElementById("id_description_elements-3-show_title").checked = true;
    document.getElementById("id_description_elements-3-title").value = "You May Also Like";

    document.getElementById("id_description_elements-5-show_title").checked = true;
    document.getElementById("id_description_elements-5-title").value = "Additional Options";

    document.getElementById("id_description_elements-8-show_title").checked = true;
    document.getElementById("id_description_elements-8-title").value = "Why Choose Us";

    document.getElementById("id_description_elements-10-show_title").checked = true;
    document.getElementById("id_description_elements-10-title").value = "How It Works";

    document.getElementById("id_description_elements-12-show_title").checked = true;
    document.getElementById("id_description_elements-12-title").value = "Completion Time";

    document.getElementById("id_description_elements-14-show_title").checked = true;

    if (aboutTitle != "")
        document.getElementById("id_description_elements-14-title").value = aboutTitle;

    document.getElementById("id_description_elements-16-show_title").checked = true;
    document.getElementById("id_description_elements-16-title").value = "FAQs";
}

let _status = true;

document.getElementById("main").addEventListener("click", () => {
    _status = true;
});
