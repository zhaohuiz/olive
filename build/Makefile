thisdir = build
SUBDIRS = 
include ../build/rules.make

BUILT_FILES = common/Consts.cs

all-local install-local test-local run-test-local run-test-ondotnet-local uninstall-local: $(BUILT_FILES)
	@:

clean-local:
	-rm -f $(BUILT_FILES)


distclean-local: clean-local
	rm -f config.make deps/* deps/.stamp

common/Consts.cs: common/Consts.cs.in $(wildcard config.make)
	test -n '$(MONO_VERSION)'
	sed -e 's,@''MONO_VERSION@,$(MONO_VERSION),' $< > $@

PLATFORMS = linux win32
PROFILES = net_3_0 net_2_1 net_3_5

COMMON_SRCS = \
	Consts.cs.in			\
	Locale.cs			\
	MonoTODOAttribute.cs		\
	TestConfiguration.cs

DISTFILES = \
	config-default.make		\
	executable.make			\
	library.make			\
	rules.make			\
	corcompare.make			\
	$(COMMON_SRCS:%=common/%)	\
	$(PLATFORMS:%=platforms/%.make) $(PROFILES:%=profiles/%.make)

dist-local: dist-default
	$(MKINSTALLDIRS) $(distdir)/deps
