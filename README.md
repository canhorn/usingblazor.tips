# Using Blazor Tips Website

# About 

This is a website that has a collection of Blazor tips. Each tips comes with details, Source Code, and a example if doable.

This project is built on Blazor Wasm, when the application is published it will generate html files for each marked page. This is a merger of a Static Site Generator, the files the get generated are self contained Blazor Wasm application. But after the first request the page starts functioning like a Single Page Application, in that it does not do a full page request when navigating in the application. The framework that this generation is built on could be changed to support full static generation, but the added benefit of being able to write C# to add features is where the Blazor Wasm shines.

# Site Generation Features

- Sidebar that will auto update based on razor mnnotated with the [PageAttribute] Attribute.
- Static Site Generation for Blazor Wasm
- Localzation Built-In
- Utility Components Available for Markdown and general debugging display.

# Support

This project supports Blazor Server, Wasm, and Static Site Wasm deployments.

# Usage

The the only required step is to create a razor file with a [Page] attribute and inherit from the PageMetadataBase class.

## Metadata

Including [PageMetadata] attribute, set meta information about the page.

### Supported Metadata

Property | Description
--- | ---
Title | Used for display purposes outside of this page.

## Example Razor File

**CreateAMap.razor**
~~~ html
@page "/tutorials/create-a-map"
@attribute [Page]
@attribute [PageMetadata(Title = "Create a Map")]
@inherits PageMetadataBase

<h1>@Localizer["Create a Map"]</h1>

<p>
    @Localizer["Tutorial on how to create a map..."]
</p>

~~~

# Application Details

This project created a Side Navigation based on the routes of the pages, so it will nest the routes under "folder" creating a compact tree structure. 

The folders, pulled from the route, will require Resource Keys to be added to the SharedResource.resx to correctly localize.

# Deployment Scenarios

You can clone this project and run the solution, checkout the GettingStarted.razor and CreateAMap.razor for examples of how the pages are structured. Any pages, correctly attributed, in the Pages directory should be supported.

# Creating a Blazor Server Hosted Docker Image

I have included a Dockerfile, that can be used to package up the generated docs site for easy usage in just about any environment.

~~~ bash
docker build -t <docker-org>/docs:latest .
~~~

# Push a Built Docker Image to the Docker Hub Registry

~~~ bash
docker push <docker-org>/docs:latest
~~~

# Generate Static Pre-Rendered Output Files

This process uses the Static.PreRenderer project to spin up an InMemory Host of the Server Project, that that then goes through all the registered Routes generating a base, a gzipped compressed and a brotli compressed version of the page into the output/wwwroot folder.

After running the command you can find the generate source in the ./output/wwwroot folder, you can then take that generated source and publish it into any static host provider.

~~~ bash
# Linux: Using sh you can generate the files
sh publish.sh
~~~

~~~ bash
# Use the dotnet serve tool to host a static version of the site
dotnet serve --directory ./output/wwwroot
~~~
