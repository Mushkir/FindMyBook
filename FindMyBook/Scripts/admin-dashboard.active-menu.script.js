/*@* Function for highlight active menu * @*/
    function activeMenu(menuElementName, tagElementName) {
        menuElementName.style.backgroundColor = "#E5EDFA"
        menuElementName.classList.add("rounded-top");
        menuElementName.classList.add("text-black")
        tagElementName.classList.add("text-black", "fw-semibold")
    }

/*@* Function for highligh dropdown menu active * @*/
    function dropDownActiveMenu(menuHeaderElementName, mainMenuElementName, menuElementName, tagElementName) {
        //@*
        //        * menuHeaderElementName means heading of dropdown menu.It is inside an ANCHOR TAG(Ex: Books, Customers)
        //    * mainMenuElementName means Main Menu list element id name.
        //        * menuElementName means Sub - Element id name(Ex: Add books, Add customers).
        //        * tagElementName means Sub - Element tag id name.
        //    * @

            mainMenuElementName.style.backgroundColor = "#E5EDFA"
        mainMenuElementName.classList.add("rounded-top");
        menuHeaderElementName.classList.add("text-black", "fw-semibold")

        menuElementName.style.backgroundColor = "#E5EDFA"
        menuElementName.classList.add("text-black")
        tagElementName.classList.add("text-black", "fw-semibold")
    }

const PATH_ORIGIN = window.location.origin;
const PATH_URL = window.location.href;

const dashboardMenuEl = document.querySelector("#dashboard-menu")
const addBookMenuEl = document.querySelector("#add-book-menu")
const bookHeaderEl = document.querySelector("#book-header")
const customerHeaderEl = document.querySelector("#customer-header")
const addCustomerMenuEl = document.querySelector("#add-customer-menu")
const addAuthorMenuEl = document.querySelector("#add-author-menu")
const authorMenuEl = document.querySelector("#author-menu");
const authorHeaderEl = document.querySelector("#author-header");

const publisherHeaderEl = document.querySelector("#publisher-header")
const publisherMenuEl = document.querySelector("#publisher-menu")
const addPublisherMenuEl = document.querySelector("#add-publisher-menu")


if (PATH_URL == `${PATH_ORIGIN}/Admin/Dashboard`) {
    //style = "background-color: #E5EDFA"
    const overviewTagEl = document.querySelector("#overview-tag");

    activeMenu(dashboardMenuEl, overviewTagEl)


} else if (PATH_URL == `${PATH_ORIGIN}/Book/AddBook`) {

    const addBookTagEl = document.querySelector("#add-book-tag")
    const bookMenuEl = document.querySelector("#book-menu");

    dropDownActiveMenu(bookHeaderEl, bookMenuEl, addBookMenuEl, addBookTagEl);

} else if (PATH_URL == `${PATH_ORIGIN}/Admin/AddCustomer`) {

    const customerMenuEl = document.querySelector("#customer-menu")
    const addCustomerTagEl = document.querySelector("#add-customer-tag")

    dropDownActiveMenu(customerHeaderEl, customerMenuEl, addCustomerMenuEl, addCustomerTagEl);

} else if (PATH_URL == `${PATH_ORIGIN}/Author/AddAuthor`) {

    const addAuthorTagEl = document.querySelector("#add-author-tag");

    dropDownActiveMenu(authorHeaderEl, authorMenuEl, addAuthorMenuEl, addAuthorTagEl);

} else if (PATH_URL == `${PATH_ORIGIN}/Author/ManageAuthors`) {

    const manageAuthorMenuEl = document.querySelector("#manage-author-menu");
    const manageAuthorTagEl = document.querySelector("#manage-author-tag");

    dropDownActiveMenu(authorHeaderEl, authorMenuEl, manageAuthorMenuEl, manageAuthorTagEl);

} else if (PATH_URL == `${PATH_ORIGIN}/Publisher/Create`) {

    const addPublisherTagEl = document.querySelector("#add-publisher-tag")

    dropDownActiveMenu(publisherHeaderEl, publisherMenuEl, addPublisherMenuEl, addPublisherTagEl);

} else if (PATH_URL == `${PATH_ORIGIN}/Publisher`) {

    const managePublisherMenuEl = document.querySelector("#manage-publisher-menu");
    const managePublisherTagEl = document.querySelector("#manage-publisher-tag");

    dropDownActiveMenu(publisherHeaderEl, publisherMenuEl, managePublisherMenuEl, managePublisherTagEl);
}