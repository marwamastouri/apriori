# R mirror only has old versions, so we use GitHub instead
# R Tools must be installed to compile the sources
# https://cran.r-project.org/bin/windows/Rtools/
# the latest Microsoft R Open distribution can be found here: 
# https://mran.microsoft.com/download

if (!require("devtools")) install.packages("devtools")
# update devtools
devtools::install_github("r-lib/debugme")
devtools::install_github("r-lib/processx")
devtools::install_github("r-lib/crayon")
devtools::install_github("mllg/backports")
devtools::install_github("r-lib/desc")
devtools::install_github("r-lib/rematch2")
devtools::install_github("r-lib/gh")
devtools::install_github("r-lib/usethis")
devtools::install_github("hadley/devtools")

# install ggalt
devtools::install_github("tidyverse/ggplot2")
devtools::install_github("wch/extrafontdb")
devtools::install_github("wch/Rttf2pt1")
devtools::install_github("s-u/base64enc")
devtools::install_github("RcppCore/Rcpp")
devtools::install_github("r-lib/later")
devtools::install_github("rstudio/promises")
devtools::install_github("rstudio/httpuv")
devtools::install_github("rstudio/shiny")
devtools::install_github("ramnathv/htmlwidgets")
devtools::install_github("tidyverse/purrr")
devtools::install_github("tidyverse/glue")
devtools::install_github("tidyverse/tidyselect")
devtools::install_github("tidyverse/tidyr")
devtools::install_github("hrbrmstr/ggalt")

# install ggally
devtools::install_github("r-lib/pkgconfig")
devtools::install_github("tidyverse/hms")
devtools::install_github("gaborcsardi/prettyunits")
devtools::install_github("r-lib/progress")
devtools::install_github("hadley/reshape")
if (!require("hadley/reshape")) install.packages("hadley/reshape")
devtools::install_github("ggobi/ggally")

# clustering algorithms
devtools::install_github("mhahsler/dbscan")
devtools::install_github("snoweye/EMCluster")
install.packages("PPtree")
install.packages("NMI")
