#![feature(custom_attribute, proc_macro_hygiene, decl_macro)]
mod routes;
#[macro_use] extern crate rocket;

fn main() {
    rocket::ignite()
        .mount("/", routes::set_up())
        .launch();
}
