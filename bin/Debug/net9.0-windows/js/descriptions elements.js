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

    textareaRequirements.value = requirements;
    textareaAdditionalOptions.value = addiptionalOptions;
    textareaAbout.value = about;
    
    document.getElementById('cke_400').click();
    document.getElementById('cke_704').click();
    document.getElementById('cke_1388').click();
}

function changeBoostingMethod(block)
{
    var selectElement = document.getElementById('id_description_elements-6-type');

    if (block == 1) { selectElement.value = 'blocks_one'; }
    else if (block == 2) { selectElement.value = 'blocks_two'; }

    var event = new Event('change', { bubbles: true });
    selectElement.dispatchEvent(event);
}

async function executeFunction(trigger, position, title, body) 
{
    document.querySelector(trigger).click();
    await delay(2000);

    var relatedPopupDiv = document.querySelector('.related-popup');
    var iframe = relatedPopupDiv.querySelector('iframe');
    var iframeDocument = iframe.contentDocument || iframe.contentWindow.document;

    iframeDocument.querySelector('input[name="pos"]').value = position;
    iframeDocument.querySelector('input[name="title"]').value = title;
    iframeDocument.getElementById('cke_19').click();
    await delay(500);

    iframeDocument.querySelector('textarea[title="Rich Text Editor, id_content"]').value = body;
    iframeDocument.getElementById('cke_19').click();
    await delay(500);

    iframeDocument.querySelector('input[type="submit"][value="Save"]').click();
    await delay(500);
}

javascript:(function()
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
    document.getElementById("id_description_elements-14-title").value = "{&aboutTitle&}";

    document.getElementById("id_description_elements-16-show_title").checked = true;
    document.getElementById("id_description_elements-16-title").value = "FAQs";
})();

changeBoostingMethod("{&boostingMethod&}");
insertText("{&requirements&}", "{&additionalOptions&}", "{&aboutText&}");
