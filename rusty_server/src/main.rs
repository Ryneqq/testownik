#![feature(custom_attribute, proc_macro_hygiene, decl_macro)]
mod routes;
use rocket;

fn main() {
    println!("Hello, world!");
    rocket::ignite().mount("/", routes::set_up()).launch();
}
