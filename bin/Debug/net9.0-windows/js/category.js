function delay(ms)
{
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function insertText(shortDescription, pageDescription)
{
    document.getElementById('cke_23').click();
    document.getElementById('cke_100').click();

    await delay(1000);

    let textareaShortDescription = document.querySelector('textarea[title="Rich Text Editor, id_short_description"]');
    let textareaPageDescription = document.querySelector('textarea[title="Rich Text Editor, id_page_description_block"]');

    if (shortDescription != "")
        textareaShortDescription.value = shortDescription;
    if (pageDescription != "")
        textareaPageDescription.value = pageDescription;

    document.getElementById('cke_23').click();
    document.getElementById('cke_100').click();
}

function insertStaticText(url, title, seotitle, seotext)
{
    if (url != "")
        document.getElementById("id_codename").value = url;

    if (title != "")
        document.getElementById("id_title_h1").value = title;

    if (seotitle != "")
        document.getElementById("id_title_seo").value = seotitle;

    if (seotext != "")
        document.getElementById("id_seo_text").value = seotext;
}

