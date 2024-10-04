function delay(ms)
{
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function insertText(advantages, description, reward)
{
    document.getElementById('cke_27').click();
    document.getElementById('cke_104').click();
    document.getElementById('cke_180').click();

    await delay(1000);

    let textareaAdvantages = document.querySelector('textarea[title="Rich Text Editor, id_short_description"]');
    let textareaDescription = document.querySelector('textarea[title="Rich Text Editor, id_description"]');
    let textareaReward = document.querySelector('textarea[title="Rich Text Editor, id_rewards_description"]');

    if (advantages != "")
        textareaAdvantages.value = advantages;
    if (description != "")
        textareaDescription.value = description;
    if (reward != "")
        textareaReward.value = reward;

    document.getElementById('cke_27').click();
    document.getElementById('cke_104').click();
    document.getElementById('cke_180').click();
}

function insertStaticText(possition, url, preview, title, seoTitle, seoDescription)
{
    document.getElementById("id_hidden").checked = true;

    if (possition != "")
        document.getElementById("id_pos").value = possition;

    if (url != "")
        document.getElementById("id_codename").value = url;

    if (preview != "")
        document.getElementById("id_short_title").value = preview;

    if (title != "")
        document.getElementById("id_title").value = title;

    if (seoTitle != "")
        document.getElementById("id_title_seo").value = seoTitle;

    if (seoDescription != "")
        document.getElementById("id_description_seo").value = seoDescription;
}

